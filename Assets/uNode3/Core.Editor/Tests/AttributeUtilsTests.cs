using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using MaxyGames.UNode; // Make sure this namespace is correct for AttributeUtils

// Test classes
[ComVisible(false)]
public class ComVisibleFalseTestClass { }

[ComVisible(true)]
public class ComVisibleTrueTestClass { }

public class NoComVisibleTestClass { }

[ComVisible(false)]
public class AnotherComVisibleFalseTestClass : NoComVisibleTestClass { }

public class InheritsComVisibleTrue : ComVisibleTrueTestClass { }

[ComVisible(true)] // This should override the base
public class InheritsAndOverridesComVisibleFalse : ComVisibleFalseTestClass { }


public class AttributeUtilsTests {
    [Test]
    public void GetTypesWithComVisibleAttributeFalse_FiltersCorrectly() {
        // Arrange
        var types = new List<Type> {
            typeof(ComVisibleFalseTestClass),
            typeof(ComVisibleTrueTestClass),
            typeof(NoComVisibleTestClass),
            typeof(AnotherComVisibleFalseTestClass),
            typeof(InheritsComVisibleTrue),
            typeof(InheritsAndOverridesComVisibleFalse),
            null // Test with a null type
        };

        // Act
        var filteredTypes = AttributeUtils.GetTypesWithComVisibleAttributeFalse(types).ToList();

        // Assert
        Assert.IsNotNull(filteredTypes, "Filtered list should not be null.");
        Assert.AreEqual(2, filteredTypes.Count, "Should find 2 types with ComVisible(false).");
        Assert.IsTrue(filteredTypes.Contains(typeof(ComVisibleFalseTestClass)), "ComVisibleFalseTestClass should be in the list.");
        Assert.IsTrue(filteredTypes.Contains(typeof(AnotherComVisibleFalseTestClass)), "AnotherComVisibleFalseTestClass should be in the list.");

        Assert.IsFalse(filteredTypes.Contains(typeof(ComVisibleTrueTestClass)), "ComVisibleTrueTestClass should NOT be in the list.");
        Assert.IsFalse(filteredTypes.Contains(typeof(NoComVisibleTestClass)), "NoComVisibleTestClass should NOT be in the list.");
        Assert.IsFalse(filteredTypes.Contains(typeof(InheritsComVisibleTrue)), "InheritsComVisibleTrue should NOT be in the list.");
        Assert.IsFalse(filteredTypes.Contains(typeof(InheritsAndOverridesComVisibleFalse)), "InheritsAndOverridesComVisibleFalse should NOT be in the list.");
    }

    [Test]
    public void GetTypesWithComVisibleAttributeFalse_HandlesEmptyInput() {
        // Arrange
        var types = new List<Type>();

        // Act
        var filteredTypes = AttributeUtils.GetTypesWithComVisibleAttributeFalse(types).ToList();

        // Assert
        Assert.IsNotNull(filteredTypes);
        Assert.AreEqual(0, filteredTypes.Count);
    }

    [Test]
    public void GetTypesWithComVisibleAttributeFalse_HandlesNullInput() {
        // Arrange
        IEnumerable<Type> types = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => {
            AttributeUtils.GetTypesWithComVisibleAttributeFalse(types).ToList();
        });
    }
}
