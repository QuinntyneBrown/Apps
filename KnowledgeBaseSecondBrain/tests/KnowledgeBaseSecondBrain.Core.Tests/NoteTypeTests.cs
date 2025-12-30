// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KnowledgeBaseSecondBrain.Core.Tests;

public class NoteTypeTests
{
    [Test]
    public void NoteType_Text_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Text;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(0));
    }

    [Test]
    public void NoteType_Concept_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Concept;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(1));
    }

    [Test]
    public void NoteType_Reference_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Reference;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(2));
    }

    [Test]
    public void NoteType_Meeting_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Meeting;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(3));
    }

    [Test]
    public void NoteType_Project_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Project;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(4));
    }

    [Test]
    public void NoteType_Literature_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Literature;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(5));
    }

    [Test]
    public void NoteType_Daily_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Daily;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(6));
    }

    [Test]
    public void NoteType_Permanent_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Permanent;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(7));
    }

    [Test]
    public void NoteType_Fleeting_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Fleeting;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(8));
    }

    [Test]
    public void NoteType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var noteType = NoteType.Other;

        // Assert
        Assert.That((int)noteType, Is.EqualTo(9));
    }

    [Test]
    public void NoteType_CanBeAssignedToNote()
    {
        // Arrange
        var note = new Note();

        // Act
        note.NoteType = NoteType.Concept;

        // Assert
        Assert.That(note.NoteType, Is.EqualTo(NoteType.Concept));
    }

    [Test]
    public void NoteType_AllValuesCanBeAssigned()
    {
        // Arrange
        var note = new Note();
        var allTypes = new[]
        {
            NoteType.Text,
            NoteType.Concept,
            NoteType.Reference,
            NoteType.Meeting,
            NoteType.Project,
            NoteType.Literature,
            NoteType.Daily,
            NoteType.Permanent,
            NoteType.Fleeting,
            NoteType.Other
        };

        // Act & Assert
        foreach (var type in allTypes)
        {
            note.NoteType = type;
            Assert.That(note.NoteType, Is.EqualTo(type));
        }
    }
}
