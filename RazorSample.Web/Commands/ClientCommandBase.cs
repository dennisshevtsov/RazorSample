﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RazorSample.Web.Commands
{
  public abstract class ClientCommandBase
  {
    public Guid ClientId { get; set; }

    [Required]
    public string ClientNo { get; set; }
    public string OrganizationNo { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public Guid? ClientOwnerId { get; set; }
  }
}
