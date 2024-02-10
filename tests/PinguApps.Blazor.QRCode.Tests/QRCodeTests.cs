using AngleSharp.Css.Values;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Drawing;

namespace PinguApps.Blazor.QRCode.Tests;

public class QRCodeTests
{
    [Fact]
    public void DefaultParameters()
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>();

        // Assert
        var svgElement = component.Find("svg");
        Assert.NotNull(svgElement);
        Assert.Equal("100%", svgElement.GetAttribute("width"));
        Assert.Equal("100%", svgElement.GetAttribute("height"));
        Assert.Equal("0 0 23 23", svgElement.GetAttribute("viewBox"));
        Assert.Equal("QR Code", svgElement.GetAttribute("aria-label"));
        Assert.Equal("#ffffff", component.Find("rect").GetAttribute("fill"));
        Assert.Equal("#000000", component.Find("path").GetAttribute("fill"));
    }

    [Fact]
    public void HasRole()
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>();

        // Assert
        var svgElement = component.Find("svg");
        Assert.NotNull(svgElement);
        Assert.Equal("img", svgElement.GetAttribute("role"));
    }

    [Fact]
    public void HigherErrorCorrectionHigherComplexity()
    {
        // Arrange
        using var context = new TestContext();
        var testData = "This is some data which needs to be encoded. We are writing a large enough string such that different error correction levels require different data to get the same data encoded and represented by the QR code.";

        // Act
        var componentLow = context.RenderComponent<QRCode>(parameters => parameters
            .Add(x => x.ErrorCorrection, ErrorCorrection.Low)
            .Add(x => x.Data, testData));
        var componentMedium = context.RenderComponent<QRCode>(parameters => parameters
            .Add(x => x.ErrorCorrection, ErrorCorrection.Medium)
            .Add(x => x.Data, testData));
        var componentQuartile = context.RenderComponent<QRCode>(parameters => parameters
            .Add(x => x.ErrorCorrection, ErrorCorrection.Quartile)
            .Add(x => x.Data, testData));
        var componentHigh = context.RenderComponent<QRCode>(parameters => parameters
            .Add(x => x.ErrorCorrection, ErrorCorrection.High)
            .Add(x => x.Data, testData));

        // Assert
        var pathDataLow = componentLow.Find("path").GetAttribute("d");
        var pathDataMedium = componentMedium.Find("path").GetAttribute("d");
        var pathDataQuartile = componentQuartile.Find("path").GetAttribute("d");
        var pathDataHigh = componentHigh.Find("path").GetAttribute("d");

        Assert.NotEqual(pathDataLow, pathDataMedium);
        Assert.NotEqual(pathDataLow, pathDataQuartile);
        Assert.NotEqual(pathDataLow, pathDataHigh);
        Assert.NotEqual(pathDataMedium, pathDataQuartile);
        Assert.NotEqual(pathDataMedium, pathDataHigh);
        Assert.NotEqual(pathDataQuartile, pathDataHigh);

        Assert.True(pathDataLow?.Length < pathDataMedium?.Length, "Expected high error correction to produce a more complex QR code.");
        Assert.True(pathDataMedium?.Length < pathDataQuartile?.Length, "Expected high error correction to produce a more complex QR code.");
        Assert.True(pathDataQuartile?.Length < pathDataHigh?.Length, "Expected high error correction to produce a more complex QR code.");
    }

    [Fact]
    public void DifferentDataEncodesDifferentResult()
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var testData1 = "Hello World!";
        var component1 = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Data, testData1));

        var testData2 = "Different Data";
        var component2 = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Data, testData2));

        // Assert: Retrieve the SVG path data for each component and compare
        var pathData1 = component1.Find("path").GetAttribute("d");
        var pathData2 = component2.Find("path").GetAttribute("d");

        Assert.NotEqual(pathData1, pathData2);
    }

    [Theory]
    [InlineData("100px")]
    [InlineData("5rem")]
    [InlineData("50%")]
    [InlineData("auto")]
    public void AppliesCustomSize(string size)
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Size, size));

        // Assert
        var width = component.Find("svg").GetAttribute("width");
        var height = component.Find("svg").GetAttribute("height");

        Assert.Equal(width, size);
        Assert.Equal(height, size);
    }

    [Theory]
    [InlineData("#ff0000")]
    [InlineData("green")]
    [InlineData("#00f")]
    public void AppliesCustomForeColor(string color)
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.ForeColor, color));

        // Assert
        Assert.Equal(color, component.Find("path").GetAttribute("fill"));
    }

    [Theory]
    [InlineData("#ff0000")]
    [InlineData("green")]
    [InlineData("#00f")]
    public void AppliesCustomBackColor(string color)
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.BackColor, color));

        // Assert
        Assert.Equal(color, component.Find("rect").GetAttribute("fill"));
    }

    [Theory]
    [InlineData("my-class")]
    [InlineData("custom multiple classes")]
    [InlineData("mb-2")]
    public void AppliesCustomClass(string className)
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Class, className));

        // Assert
        Assert.Equal(className, component.Find("svg").GetAttribute("class"));
    }

    [Fact]
    public void AppliesCustomId()
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var id = "my-id";
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Id, id));

        // Assert
        Assert.Equal(id, component.Find("svg").GetAttribute("id"));
    }

    [Theory]
    [InlineData("My description")]
    [InlineData("a 2FA code to setup for your account")]
    public void AppliesCustomAriaDescription(string ariaDescription)
    {
        // Arrange
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.AriaDescription, ariaDescription));

        // Assert
        Assert.Equal(ariaDescription, component.Find("svg").GetAttribute("aria-label"));
    }

    [Theory]
    [InlineData("Hello World!")]
    [InlineData("https://www.google.com/")]
    [InlineData("{\"foo\":\"bar\",\"baz\":69}")]
    public async Task DataSnapshots(string data)
    {
        using var context = new TestContext();

        // Act
        var component = context.RenderComponent<QRCode>(parameters => parameters
            .Add(p => p.Data, data));

        // Assert
        var settings = new VerifySettings();
        settings.UseParameters(data);
        await Verify(component.Find("path").GetAttribute("d"), settings);
    }
}