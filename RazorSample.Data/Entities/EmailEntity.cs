﻿using System;

namespace RazorSample.Data.Entities
{
  public sealed class EmailEntity
  {
    public Guid EmailId { get; set; }
    public Guid SubjectId { get; set; }

    public string Email { get; set; }
    public string Description { get; set; }
  }
}
