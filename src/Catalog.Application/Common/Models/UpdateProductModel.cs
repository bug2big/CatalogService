﻿namespace Catalog.Application.Common.Models;

public class UpdateProductModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public double? Price { get; set; }
    public int? Amount { get; set; }
}
