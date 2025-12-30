// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AccountCategoryTests
{
    [Test]
    public void AccountCategory_AllValues_CanBeAssigned()
    {
        // Arrange
        var categories = new[]
        {
            AccountCategory.SocialMedia,
            AccountCategory.Email,
            AccountCategory.Banking,
            AccountCategory.Shopping,
            AccountCategory.Work,
            AccountCategory.Entertainment,
            AccountCategory.Healthcare,
            AccountCategory.Other
        };

        // Act & Assert
        foreach (var category in categories)
        {
            var account = new Account { Category = category };
            Assert.That(account.Category, Is.EqualTo(category));
        }
    }
}
