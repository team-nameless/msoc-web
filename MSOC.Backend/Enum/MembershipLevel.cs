namespace MSOC.Backend.Enum;

/// <summary>
///     MSOC membership tiers, to prove your loyalty to MSOC.
/// </summary>
[Flags]
public enum MembershipLevel
{
    /// <summary>
    ///     MSOC
    /// </summary>
    MSOC = 1 << 1,
    
    /// <summary>
    ///     MSOC PLUS
    /// </summary>
    MSOC_PLUS = MSOC | (1 << 2),
    
    /// <summary>
    ///     MSOC PLUS+
    /// </summary>
    MSOC_PLUS_PLUS = MSOC_PLUS | (1 << 3),
    
    /// <summary>
    ///     MSOC PRO
    /// </summary>
    MSOC_PRO = MSOC_PLUS | (1 << 4),
    
    /// <summary>
    ///     MSOC ULTIMATE
    /// </summary>
    MSOC_ULTIMATE = MSOC_PRO | (1 << 5),
    
    /// <summary>
    ///     MSOC UNLIMITED
    /// </summary>
    MSOC_UNLIMITED = MSOC_ULTIMATE | (1 << 6),
    
    /// <summary>
    ///     MSOC "SUPER DREAM TEAM"
    /// </summary>
    MSOC_SUPER_DREAM_TEAM = MSOC_UNLIMITED | (1 << 7),
}