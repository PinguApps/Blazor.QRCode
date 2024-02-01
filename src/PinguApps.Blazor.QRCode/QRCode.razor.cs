using Microsoft.AspNetCore.Components;

namespace PinguApps.Blazor.QRCode;

/// <summary>
/// A Blazor component for generating QR codes. This component encodes specified data into a QR code image,
/// which can be customized in terms of size, colors, and error correction level. The QR code is generated as an SVG for optimal scalability and clarity.
/// Key features include:
/// - Customizable error correction level, allowing the QR code to remain readable even when partially obscured or damaged.
/// - Data encoding capability to transform any given string into a QR code.
/// - Adjustable height and width of the QR code, with a default setting of 100% for both, making it responsive to the container's size.
/// - The ability to specify the number of padding cells around the QR code for better isolation and visibility.
/// - Options to customize the foreground and background colors of the QR code, providing versatility for different design needs.
/// - Support for additional CSS class and ID on the QR code container, enabling easy styling and identification within HTML.
/// The component uses <see cref="Net.Codecrete.QrCodeGenerator"/> for QR code generation and provides an easy-to-use interface for integrating QR codes in Blazor applications.
/// </summary>
public partial class QRCode : ComponentBase
{
}