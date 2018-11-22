using Mapster;
using Microsoft.AspNetCore.Mvc;
using RazorSample.Data.Entities;
using RazorSample.Hr;
using RazorSample.Random;
using RazorSample.Vm;
using RazorSample.Web.Commands;
using RazorSample.Web.Extensions;
using RazorSample.Web.Queries;
using RazorSample.Web.Services;
using System;
using System.Threading.Tasks;

namespace RazorSample.Web.Controllers
{
  public sealed class EmployeeController : Controller
  {
    private readonly IResourceBuilder _builder;
    private readonly IEmployeeService _employeeService;
    private readonly IRandomGenerator _randomGenerator;

    public EmployeeController(
      IResourceBuilder builder,
      IEmployeeService employeeService,
      IRandomGenerator randomGenerator)
    {
      _builder = builder ?? throw new ArgumentNullException(nameof(builder));
      _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
      _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
    }

    [HttpGet]
    public async Task<IActionResult> Index(SearchEmployeeQuery query)
    {
      _builder.Link(RelTypes.Nav, "Employees", SearchUri(query))
              .Link(Url.AppLink(RelTypes.Nav, "Client", "index", "client"))
              .Link(RelTypes.Breadcrumb, "Employees", SearchUri(query))
              .Link(RelTypes.Action, "+ new employee", AddUri())
              .Link(RelTypes.Self, "Employees", SearchUri(query))
              .Link(RelTypes.Search, "Search", SearchUri(new SearchEmployeeQuery(nameof(SearchEmployeeQuery.EmployeeNo))));

      var queryExecutionResult = await _employeeService.HandleAsync(query);

      if (queryExecutionResult.HasError == false)
      {
        if (queryExecutionResult.Result.PageNo > queryExecutionResult.Result.FirstPageNo)
        {
          _builder.Link(RelTypes.First, "First", SearchUri(new SearchEmployeeQuery(query.EmployeeNo, queryExecutionResult.Result.FirstPageNo)))
                  .Link(RelTypes.Prev, "Previous", SearchUri(new SearchEmployeeQuery(query.EmployeeNo, queryExecutionResult.Result.PageNo - 1)));
        }

        if (queryExecutionResult.Result.PageNo < queryExecutionResult.Result.LastPageNo)
        {
          _builder.Link(RelTypes.Next, "Next", SearchUri(new SearchEmployeeQuery(query.EmployeeNo, queryExecutionResult.Result.PageNo + 1)))
                  .Link(RelTypes.Last, "Last", SearchUri(new SearchEmployeeQuery(query.EmployeeNo, queryExecutionResult.Result.LastPageNo)));
        }

        foreach (var employee in queryExecutionResult.Result)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("name", "Name", employee.FullName)
                  .Property(nameof(employee.EmployeeNo), "Employee No", employee.EmployeeNo)
                  .Property(nameof(employee.Created), "Created", employee.Created)
                  .Link(RelTypes.Self, employee.FullName, GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employee.EmployeeId)))
                  .Link(Url.AppLink(RelTypes.Nav, "+ new client", nameof(ClientController.Add), nameof(ClientController), new CreateClientQuery(employee.EmployeeId)));
        }
      }

      var vm = _builder.Build()
                       .ToGridVm();

      return View(vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var command = _randomGenerator.RandomEmployee();
      var vm = BuildAddForm(command);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateEmployeeCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddForm(command);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> GeneralInfo(UpdateEmployeeGeneralInfoQuery query)
    {
      var employeeEntity = await _employeeService.HandleAsync(query);
      var vm = BuildGeneralInfoForm(employeeEntity.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> GeneralInfo(UpdateEmployeeGeneralInfoQuery query, UpdateEmployeeGeneralInfoCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var employeeEntity = command.Adapt<EmployeeEntity>();
        var vm = BuildGeneralInfoForm(employeeEntity);

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return RedirectToAction(nameof(Index), new SearchEmployeeQuery(command.EmployeeNo));
    }

    [HttpGet]
    public async Task<IActionResult> Addresses(SearchEmployeeAddressQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildAddressesVm(queryExecutionResult.Result);

      return View("GridView", vm);
    }

    [HttpGet]
    public async Task<IActionResult> AddAddress(AddEmployeeAddressQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildNewAddressVm(queryExecutionResult.Result, new AddEmployeeAddressCommand());

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress(AddEmployeeAddressQuery query, AddEmployeeAddressCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var queryExecutionResult = await _employeeService.HandleAsync(query);
        var vm = BuildNewAddressVm(queryExecutionResult.Result, command);

        return View(vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(AddressesUri(new SearchEmployeeAddressQuery(query.EmployeeId)));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAddress(RemoveEmployeeAddressQuery query, RemoveEmployeeAddressCommand command)
    {
      await _employeeService.HandleAsync(command);

      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildAddressesVm(queryExecutionResult.Result);

      return PartialView(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Emails(SearchEmployeeEmailQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildEmailsVm(queryExecutionResult.Result);

      return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> AddEmail(AddEmployeeEmailQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildNewEmailVm(queryExecutionResult.Result, new AddEmployeeEmailCommand());

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmail(AddEmployeeEmailQuery query, AddEmployeeEmailCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var queryExecutionResult = await _employeeService.HandleAsync(query);
        var vm = BuildNewEmailVm(queryExecutionResult.Result, command);

        return View(vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(EmailsUri(new SearchEmployeeEmailQuery(query.EmployeeId)));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveEmail(RemoveEmployeeEmailQuery query, RemoveEmployeeEmailCommand command)
    {
      await _employeeService.HandleAsync(command);

      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildEmailsVm(queryExecutionResult.Result);

      return PartialView(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Phones(SearchEmployeePhoneQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildPhonesVm(queryExecutionResult.Result);

      return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> AddPhone(AddEmployeePhoneQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildNewPhoneVm(queryExecutionResult.Result, new AddEmployeePhoneCommand());

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddPhone(AddEmployeePhoneQuery query, AddEmployeePhoneCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var queryExecutionResult = await _employeeService.HandleAsync(query);
        var vm = BuildNewPhoneVm(queryExecutionResult.Result, command);

        return View(vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(PhonesUri(new SearchEmployeePhoneQuery(query.EmployeeId)));
    }

    [HttpPost]
    public async Task<IActionResult> RemovePhone(RemoveEmployeePhoneQuery query, RemoveEmployeePhoneCommand command)
    {
      await _employeeService.HandleAsync(command);

      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildPhonesVm(queryExecutionResult.Result);

      return PartialView(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Ims(SearchEmployeeImQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildImsVm(queryExecutionResult.Result);

      return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> AddIm(AddEmployeeImQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildNewImVm(queryExecutionResult.Result, new AddEmployeeImCommand());

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddIm(AddEmployeeImQuery query, AddEmployeeImCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var queryExecutionResult = await _employeeService.HandleAsync(query);
        var vm = BuildNewImVm(queryExecutionResult.Result, command);

        return View(vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(ImsUri(new SearchEmployeeImQuery(query.EmployeeId)));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveIm(RemoveEmployeeImQuery query, RemoveEmployeeImCommand command)
    {
      await _employeeService.HandleAsync(command);

      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildImsVm(queryExecutionResult.Result);

      return PartialView(vm);
    }

    private IResourceBuilder BuildPageBase() =>
      _builder.Link(RelTypes.Nav, "Employees", SearchUri(null))
              .Link(Url.AppLink(RelTypes.Nav, "Client", nameof(ClientController.Index), nameof(ClientController)))
              .Link(RelTypes.Breadcrumb, "Employees", SearchUri(null));

    private IFormVm BuildAddForm(CreateEmployeeCommand command) =>
      BuildPageBase().Link(RelTypes.Self, "New Employee", AddUri())
                     .Link(RelTypes.Breadcrumb, "New Employee", AddUri())
                     .Property(nameof(CreateEmployeeCommand.FirstName), "First Name", command.FirstName)
                     .Property(nameof(CreateEmployeeCommand.LastName), "Last Name", command.LastName)
                     .Property(nameof(CreateEmployeeCommand.EmployeeNo), "Employee No", command.EmployeeNo)
                     .Property(nameof(CreateEmployeeCommand.Email), "Email", command.Email)
                     .Build()
                     .ToFormVm();

    private IResourceBuilder BuildEditBase(EmployeeEntity employeeEntity) =>
      BuildPageBase().Link(RelTypes.Breadcrumb, employeeEntity.FullName, GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Action, "+ new employee", AddUri())
                     .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController), new CreateClientQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "General Info", GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Addresses", AddressesUri(new SearchEmployeeAddressQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Emails", EmailsUri(new SearchEmployeeEmailQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Phones", PhonesUri( new SearchEmployeePhoneQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "IM", ImsUri(new SearchEmployeeImQuery(employeeEntity.EmployeeId)));

    private IFormVm BuildGeneralInfoForm(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Employees", GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.FirstName), "First Name", employeeEntity.FirstName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.LastName), "Last Name", employeeEntity.LastName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.EmployeeNo), "Employee No", employeeEntity.EmployeeNo)
                                   .Build()
                                   .ToFormVm();

    private IResourceBuilder BuildeAddressesTab(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Addresses", AddressesUri(new SearchEmployeeAddressQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Addresses", AddressesUri(new SearchEmployeeAddressQuery(employeeEntity.EmployeeId)));

    private IPageVm BuildAddressesVm(EmployeeEntity employeeEntity)
    {
      BuildeAddressesTab(employeeEntity).Link(RelTypes.Action, "+ new address", AddAddressUri(new AddEmployeeAddressQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Addresses != null)
      {
        foreach (var address in employeeEntity.Addresses)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("Address", "Address", address.Address)
                  .Property("Zip", "Zip", address.Zip)
                  .Property("City", "City", address.City)
                  .Property("Country", "Country", address.Country)
                  .Property("Description", "Description", address.Description)
                  .Link(RelTypes.Action, "remove", RemoveAddressUri(new RemoveEmployeeAddressQuery(employeeEntity.EmployeeId, address.AddressId)));
        }
      }

      return _builder.Build()
                     .ToGridVm();
    }

    private IPageVm BuildNewAddressVm(EmployeeEntity employeeEntity, AddEmployeeAddressCommand command) =>
      BuildeAddressesTab(employeeEntity).Link(RelTypes.Breadcrumb, "New Address", AddAddressUri(new AddEmployeeAddressQuery(employeeEntity.EmployeeId)))
                                        .Property(nameof(AddEmployeeAddressCommand.Address), "Address", command.Address)
                                        .Property(nameof(AddEmployeeAddressCommand.Zip), "ZIP", command.Zip)
                                        .Property(nameof(AddEmployeeAddressCommand.City), "City", command.City)
                                        .Property(nameof(AddEmployeeAddressCommand.Country), "Country", command.Country)
                                        .Property(nameof(AddEmployeeAddressCommand.Description), "Description", command.Description)
                                        .Build()
                                        .ToFormVm();

    private IResourceBuilder BuildeEmailTab(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Emails", EmailsUri(new SearchEmployeeEmailQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Emails", EmailsUri(new SearchEmployeeEmailQuery(employeeEntity.EmployeeId)));

    private IPageVm BuildEmailsVm(EmployeeEntity employeeEntity)
    {
      BuildeEmailTab(employeeEntity).Link(RelTypes.Action, "+ new email", AddEmailUri(new AddEmployeeEmailQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Emails != null)
      {
        foreach (var email in employeeEntity.Emails)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("Email", "Email", email.Email)
                  .Property("Description", "Description", email.Description)
                  .Link(RelTypes.Action, "remove", RemoveEmailUri(new RemoveEmployeeEmailQuery(employeeEntity.EmployeeId, email.EmailId)));
        }
      }

      return _builder.Build()
                     .ToGridVm();
    }

    private IPageVm BuildNewEmailVm(EmployeeEntity employeeEntity, AddEmployeeEmailCommand command) =>
      BuildeEmailTab(employeeEntity).Link(RelTypes.Breadcrumb, "New Email", AddEmailUri(new AddEmployeeEmailQuery(employeeEntity.EmployeeId)))
                                    .Property(nameof(AddEmployeeEmailCommand.Email), "Email", command.Email)
                                    .Property(nameof(AddEmployeeEmailCommand.Description), "Description", command.Description)
                                    .Build()
                                    .ToFormVm();

    private IResourceBuilder BuildePhoneTab(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Phones", PhonesUri(new SearchEmployeePhoneQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Phones", PhonesUri(new SearchEmployeePhoneQuery(employeeEntity.EmployeeId)));

    private IPageVm BuildPhonesVm(EmployeeEntity employeeEntity)
    {
      BuildePhoneTab(employeeEntity).Link(RelTypes.Action, "+ new phone", AddPhoneUri(new AddEmployeePhoneQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Phones != null)
      {
        foreach (var phone in employeeEntity.Phones)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("Phones", "Phone", phone.Phone)
                  .Property("Description", "Description", phone.Description)
                  .Link(RelTypes.Action, "remove", RemovePhoneUri(new RemoveEmployeePhoneQuery(employeeEntity.EmployeeId, phone.PhoneId)));
        }
      }

      return _builder.Build()
                     .ToGridVm();
    }

    private IPageVm BuildNewPhoneVm(EmployeeEntity employeeEntity, AddEmployeePhoneCommand command) =>
      BuildePhoneTab(employeeEntity).Link(RelTypes.Breadcrumb, "New Email", AddEmailUri(new AddEmployeeEmailQuery(employeeEntity.EmployeeId)))
                                    .Property(nameof(AddEmployeePhoneCommand.Phone), "Phone", command.Phone)
                                    .Property(nameof(AddEmployeePhoneCommand.Description), "Description", command.Description)
                                    .Build()
                                    .ToFormVm();

    private IResourceBuilder BuildImTab(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "IM", ImsUri(new SearchEmployeeImQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "IM", ImsUri(new SearchEmployeeImQuery(employeeEntity.EmployeeId)));

    private IPageVm BuildImsVm(EmployeeEntity employeeEntity)
    {
      BuildImTab(employeeEntity).Link(RelTypes.Action, "+ new im", AddImUri(new AddEmployeeImQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Ims != null)
      {
        foreach (var im in employeeEntity.Ims)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("Ims", "IM", im.Im)
                  .Property("Description", "Description", im.Description)
                  .Link(RelTypes.Action, "remove", RemoveImUri(new RemoveEmployeeImQuery(employeeEntity.EmployeeId, im.ImId)));
        }
      }

      return _builder.Build()
                     .ToGridVm();
    }

    private IPageVm BuildNewImVm(EmployeeEntity employeeEntity, AddEmployeeImCommand command) =>
      BuildImTab(employeeEntity).Link(RelTypes.Breadcrumb, "New Email", AddImUri(new AddEmployeeImQuery(employeeEntity.EmployeeId)))
                                .Property(nameof(AddEmployeeImCommand.Im), "IM", command.Im)
                                .Property(nameof(AddEmployeeImCommand.Description), "Description", command.Description)
                                .Build()
                                .ToFormVm();

    private string SearchUri(SearchEmployeeQuery query) => Url.AppUri(nameof(Index), nameof(EmployeeController), query);
    private string AddUri() => Url.AppUri(nameof(Add), nameof(EmployeeController));
    private string GeneralInfoUri(UpdateEmployeeGeneralInfoQuery query) => Url.AppUri(nameof(GeneralInfo), nameof(EmployeeController), query);
    private string AddressesUri(SearchEmployeeAddressQuery query) => Url.AppUri(nameof(Addresses), nameof(EmployeeController), query);
    private string AddAddressUri(AddEmployeeAddressQuery query) => Url.AppUri(nameof(AddAddress), nameof(EmployeeController), query);
    private string RemoveAddressUri(RemoveEmployeeAddressQuery query) => Url.AppUri(nameof(RemoveAddress), nameof(EmployeeController), query);
    private string EmailsUri(SearchEmployeeEmailQuery query) => Url.AppUri(nameof(Emails), nameof(EmployeeController), query);
    private string AddEmailUri(AddEmployeeEmailQuery query) => Url.AppUri(nameof(AddEmail), nameof(EmployeeController), query);
    private string RemoveEmailUri(RemoveEmployeeEmailQuery query) => Url.AppUri(nameof(RemoveEmail), nameof(EmployeeController), query);
    private string PhonesUri(SearchEmployeePhoneQuery query) => Url.AppUri(nameof(Phones), nameof(EmployeeController), query);
    private string AddPhoneUri(AddEmployeePhoneQuery query) => Url.AppUri(nameof(AddPhone), nameof(EmployeeController), query);
    private string RemovePhoneUri(RemoveEmployeePhoneQuery query) => Url.AppUri(nameof(RemovePhone), nameof(EmployeeController), query);
    private string ImsUri(SearchEmployeeImQuery query) => Url.AppUri(nameof(Ims), nameof(EmployeeController), query);
    private string AddImUri(AddEmployeeImQuery query) => Url.AppUri(nameof(AddIm), nameof(EmployeeController), query);
    private string RemoveImUri(RemoveEmployeeImQuery query) => Url.AppUri(nameof(RemoveIm), nameof(EmployeeController), query);
  }
}
