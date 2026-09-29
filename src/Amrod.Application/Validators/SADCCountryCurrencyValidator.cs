namespace Amrod.Application.Validators;

public static class SADCCountryCurrencyValidator
{
    // Thread-safe dictionary mapping SADC Country Codes (Alpha-2) to official Currency Codes (ISO 4217)
    private static readonly Dictionary<string, HashSet<string>> SadcCountryCurrencyMap = 
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "AO", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "AOA" } }, // Angola (Kwanza)
            { "BW", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "BWP" } }, // Botswana (Pula)
            { "KM", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "KMF" } }, // Comoros (Franc)
            { "CD", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "CDF" } }, // DR Congo (Franc)
            { "SZ", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SZL", "ZAR" } }, // Eswatini (Lilangeni / SA Rand)
            { "LS", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "LSL", "ZAR" } }, // Lesotho (Loti / SA Rand)
            { "MG", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "MGA" } }, // Madagascar (Ariary)
            { "MW", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "MWK" } }, // Malawi (Kwacha)
            { "MU", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "MUR" } }, // Mauritius (Rupee)
            { "MZ", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "MZN" } }, // Mozambique (Metical)
            { "NA", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "NAD", "ZAR" } }, // Namibia (Dollar / SA Rand)
            { "SC", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "SCR" } }, // SC / Seychelles (Rupee)
            { "ZA", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ZAR" } }, // South Africa (Rand)
            { "TZ", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "TZS" } }, // Tanzania (Shilling)
            { "ZM", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ZMW" } }, // Zambia (Kwacha)
            { "ZW", new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ZWL", "USD" } } // Zimbabwe (ZWG / USD)
        };
    
    /// <summary>
    /// Validates if a country belongs to SADC and matches its official currency.
    /// </summary>
    /// <param name="countryCode">ISO 3166-1 Alpha-2 code (e.g., "ZA")</param>
    /// <param name="currencyCode">ISO 4217 3-letter currency code (e.g., "ZAR")</param>
    public static bool IsPairValid(string countryCode, string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(countryCode) || string.IsNullOrWhiteSpace(currencyCode))
        {
            return false;
        }

        // Check if the country is a member of SADC and if the currency is legally accepted/pegged
        return SadcCountryCurrencyMap.TryGetValue(countryCode.Trim(), out var allowedCurrencies) 
               && allowedCurrencies.Contains(currencyCode.Trim());
    }
}
    
