namespace Mentat.Server.Model;

/// <summary>
/// Represents the input data or parameters required for a specific operation or process.
/// </summary>
public class Input
{
    /// <summary>
    /// Text content that serves as the input for the operation.
    /// </summary>
    public required string Text { get; set; }
}