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
              .Link(RelTypes.Self, "Employees", SearchUri(query));

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
                  .Link(Url.AppLink(RelTypes.Action, "+ new client", nameof(ClientController.Add), nameof(ClientController), new CreateClientQuery(employee.EmployeeId)));
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
    public async Task<IActionResult> Addresses(UpdateEmployeeAddressesQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildAddressesVm(queryExecutionResult.Result);

      return View("GridView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Addresses(UpdateEmployeeAddressesQuery query, UpdateEmployeeAddressesCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddressesVm(command.Adapt<EmployeeEntity>());

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(AddressesUri(query));
    }

    [HttpGet]
    public async Task<IActionResult> Emails(UpdateEmployeeEmailsQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildEmailsForm(queryExecutionResult.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Emails(UpdateEmployeeEmailsQuery query, UpdateEmployeeEmailsCommand command)
    {
      {
        if (ModelState.IsValid == false)
        {
          var vm = BuildAddressesVm(command.Adapt<EmployeeEntity>());

          return View("FormView", vm);
        }

        await _employeeService.HandleAsync(command);

        return Redirect(EmailsUri(query));
      }
    }

    [HttpGet]
    public async Task<IActionResult> Phones(UpdateEmployeePhonesQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildPhonesForm(queryExecutionResult.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Phones(UpdateEmployeePhonesQuery query, UpdateEmployeePhonesCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddressesVm(command.Adapt<EmployeeEntity>());

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(PhonesUri(query));
    }

    [HttpGet]
    public async Task<IActionResult> Ims(UpdateEmployeeImsQuery query)
    {
      var queryExecutionResult = await _employeeService.HandleAsync(query);
      var vm = BuildImsForm(queryExecutionResult.Result);

      return View("FormView", vm);
    }

    [HttpPost]
    public async Task<IActionResult> Ims(UpdateEmployeeImsQuery query, UpdateEmployeeImsCommand command)
    {
      if (ModelState.IsValid == false)
      {
        var vm = BuildAddressesVm(command.Adapt<EmployeeEntity>());

        return View("FormView", vm);
      }

      await _employeeService.HandleAsync(command);

      return Redirect(ImsUri(query));
    }

    private IResourceBuilder BuildPageBase() =>
      _builder.Link(RelTypes.Nav, "Employees", SearchUri(null))
              .Link(Url.AppLink(RelTypes.Nav, "Client", nameof(ClientController.Index), nameof(ClientController)))
              .Link(RelTypes.Breadcrumb, "Employees", SearchUri(null));

    private IFormVm BuildAddForm(CreateEmployeeCommand command) =>
  BuildPageBase().Link(RelTypes.Self, "Employees", AddUri())
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
                     .Link(RelTypes.Tab, "General Info", GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Addresses", AddressesUri(new UpdateEmployeeAddressesQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Emails", EmailsUri(new UpdateEmployeeEmailsQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "Phones", PhonesUri( new UpdateEmployeePhonesQuery(employeeEntity.EmployeeId)))
                     .Link(RelTypes.Tab, "IM", ImsUri(new UpdateEmployeeImsQuery(employeeEntity.EmployeeId)));

    private IFormVm BuildGeneralInfoForm(EmployeeEntity employeeEntity) =>
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Employees", GeneralInfoUri(new UpdateEmployeeGeneralInfoQuery(employeeEntity.EmployeeId)))
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.FirstName), "First Name", employeeEntity.FirstName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.LastName), "Last Name", employeeEntity.LastName)
                                   .Property(nameof(UpdateEmployeeGeneralInfoCommand.EmployeeNo), "Employee No", employeeEntity.EmployeeNo)
                                   .Build()
                                   .ToFormVm();

    private IPageVm BuildAddressesVm(EmployeeEntity employeeEntity)
    {
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Addresses", AddressesUri(new UpdateEmployeeAddressesQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Addresses", AddressesUri(new UpdateEmployeeAddressesQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Addresses != null)
      {
        foreach (var address in employeeEntity.Addresses)
        {
          _builder.Embedded(RelTypes.Row)
                  .Property("Address", "Address", address.Address)
                  .Property("Zip", "Zip", address.Zip)
                  .Property("City", "City", address.City)
                  .Property("Country", "Country", address.Country)
                  .Property("Description", "Description", address.Description);
        }
      }

      return _builder.Build()
                     .ToGridVm();
    }

    private IFormVm BuildEmailsForm(EmployeeEntity employeeEntity)
    {
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Emails", EmailsUri(new UpdateEmployeeEmailsQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Emails", EmailsUri(new UpdateEmployeeEmailsQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Emails != null)
      {
        foreach (var emails in employeeEntity.Emails)
        {
          _builder.Property("Emails", "Email", emails.Email);
        }
      }

      _builder.Property("Emails", "Email", null);

      return _builder.Build()
                     .ToFormVm();
    }

    private IFormVm BuildPhonesForm(EmployeeEntity employeeEntity)
    {
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "Phones", PhonesUri(new UpdateEmployeePhonesQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "Phones", PhonesUri(new UpdateEmployeePhonesQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Phones != null)
      {
        foreach (var phone in employeeEntity.Phones)
        {
          _builder.Property("Phones", "Phone", phone.Phone);
        }
      }

      _builder.Property("Phones", "Phone", null);

      return _builder.Build()
                     .ToFormVm();
    }

    private IFormVm BuildImsForm(EmployeeEntity employeeEntity)
    {
      BuildEditBase(employeeEntity).Link(RelTypes.Self, "IM", ImsUri(new UpdateEmployeeImsQuery(employeeEntity.EmployeeId)))
                                   .Link(RelTypes.Breadcrumb, "IM", ImsUri(new UpdateEmployeeImsQuery(employeeEntity.EmployeeId)));

      if (employeeEntity.Ims != null)
      {
        foreach (var im in employeeEntity.Ims)
        {
          _builder.Property("Ims", "IM", im.Im);
        }
      }

      _builder.Property("Ims", "IM", null);

      return _builder.Build()
                     .ToFormVm();
    }

    private string SearchUri(SearchEmployeeQuery query) => Url.AppUri(nameof(Index), nameof(EmployeeController), query);
    private string AddUri() => Url.AppUri(nameof(Add), nameof(EmployeeController));
    private string GeneralInfoUri(UpdateEmployeeGeneralInfoQuery query) => Url.AppUri(nameof(GeneralInfo), nameof(EmployeeController), query);
    private string AddressesUri(UpdateEmployeeAddressesQuery query) => Url.AppUri(nameof(Addresses), nameof(EmployeeController), query);
    private string EmailsUri(UpdateEmployeeEmailsQuery query) => Url.AppUri(nameof(Emails), nameof(EmployeeController), query);
    private string PhonesUri(UpdateEmployeePhonesQuery query) => Url.AppUri(nameof(Phones), nameof(EmployeeController), query);
    private string ImsUri(UpdateEmployeeImsQuery query) => Url.AppUri(nameof(Ims), nameof(EmployeeController), query);
  }
}
