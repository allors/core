namespace Allors.Core.Database.Engines.Memory;

using System.Collections.Immutable;

/// <summary>
/// Contains the immutable state of an object.
/// </summary>
public record Store(ImmutableDictionary<long, Record> RecordById);
