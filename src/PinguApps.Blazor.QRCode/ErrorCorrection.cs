namespace PinguApps.Blazor.QRCode;

/// <summary>
/// Represents the error correction level used in a QR code.
/// Error correction allows a QR code to be read even if it is partially dirty or damaged.
/// </summary>
public enum ErrorCorrection
{
    /// <summary>
    /// Low level of error correction.
    /// Allows for approximately 7% of error correction capacity.
    /// Suitable for scenarios where there is minimal risk of the QR code getting damaged.
    /// </summary>
    Low,

    /// <summary>
    /// Medium level of error correction.
    /// Allows for approximately 15% of error correction capacity.
    /// Provides a balance between data capacity and error correction capability.
    /// </summary>
    Medium,

    /// <summary>
    /// Quartile level of error correction.
    /// Allows for approximately 25% of error correction capacity.
    /// Suitable for cases where the QR code might be exposed to more significant damage.
    /// </summary>
    Quartile,

    /// <summary>
    /// High level of error correction.
    /// Allows for approximately 30% of error correction capacity.
    /// Ideal for situations where the QR code is expected to be in a harsh environment.
    /// </summary>
    High
}