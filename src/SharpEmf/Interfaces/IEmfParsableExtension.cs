namespace SharpEmf.Interfaces;

/// <summary>
/// Provides static abstract method for parsing extension of EMF record
/// </summary>
/// <typeparam name="TBase">Type of base record</typeparam>
/// <typeparam name="TExtension">Type of extension record</typeparam>
internal interface IEmfParsableExtension<in TBase, out TExtension> where TExtension : IEmfParsableExtension<TBase, TExtension>
{
    internal static abstract TExtension Parse(Stream stream, TBase baseRecord);
}