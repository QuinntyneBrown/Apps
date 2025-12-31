// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaskPriorityMatrix.Api.Features.Categories;

public class CreateCategoryCommand
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#000000";
}
