'use strict';

function HttpClient() {
  var xhttp = new XMLHttpRequest();

  this.execute = function (uri, data, success) {
    xhttp.open('post', uri, true);
    xhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

    xhttp.onreadystatechange = function () {
      if (xhttp.readyState === XMLHttpRequest.DONE && xhttp.status === 200) {
        if (xhttp.responseURL.endsWith(uri)) {
          success(xhttp.responseText);
        } else {
          location.replace(xhttp.responseURL);
        }
      }
    };

    xhttp.send(data);
  };
}

function Controller(http) {
  var self = this;

  self.execute = function (uri, view) {
    if (self.executing) {
      self.executing(view);
    }

    http.execute(uri, view.data(), function (html) {
      view.refresh(html);

      if (self.executed) {
        self.executed(view);
      }
    });
  };
}

function Control(element) {
  var self = this;

  self.value = getValue();
  self.name = element.name;

  function getValue() {
    if (element.type === 'checkbox') {
      return !!element.checked;
    }

    return element.value;
  }
}

function View(element) {
  var self = this;

  self.data = function () {
    var data = '',
      controls = getControls(),
      i;

    for (i = 0; i < controls.length; ++i) {
      data += controls[i].name + '=' + controls[i].value + '&';
    }

    return data;
  };
  self.refresh = function (html) {
    element.innerHTML = html;

    new Bootstrap(element);
  };

  function getControls() {
    var controlElements = element.getElementsByTagName('input'),
      controls = [],
      i = 0;

    for (i = 0; i < controlElements.length; ++i) {
      controls.push(new Control(controlElements[i]));
    }

    return controls;
  }
}

function Bootstrap(element, before, after) {
  var http = new HttpClient(),
    actionElements = element.querySelectorAll('[data-hr-action]'),
    navElements = element.querySelectorAll('[data-hr-nav]'),
    i;

  for (i = 0; i < actionElements.length; ++i) {
    action(actionElements[i]);
  }

  for (i = 0; i < navElements.length; ++i) {
    nav(navElements[i]);
  }

  function viewRecursive(element) {
    var currentElement = element.parentElement;

    while (currentElement) {
      if (currentElement.hasAttribute('data-hr')) {
        if (!currentElement.view) {
          currentElement.view = new View(currentElement);

          currentElement.controller = new Controller(http);

          currentElement.controller.executing = before;
          currentElement.controller.executed = after;
        }

        return currentElement;
      }

      currentElement = currentElement.parentElement;
    }
  }

  function action(element) {
    var viewElement = viewRecursive(actionElements[i]);

    element.onclick = function () {
      var uri = element.getAttribute('data-hr-action');

      viewElement.controller.execute(uri, viewElement.view);

      return false;
    };
  }

  function nav(element) {
    if (element.tagName === 'INPUT') {
      element.onkeyup  = function (event) {
        var uri = element.getAttribute('data-hr-nav'),
            name = element.name,
            value = element.value,
            regex = new RegExp(`=${name}`, 'ig');

        if (event.keyCode === 13) {
          location.href = uri.replace(regex, `=${value}`);
        }
      };
    }
  }
}

new Bootstrap(document);
