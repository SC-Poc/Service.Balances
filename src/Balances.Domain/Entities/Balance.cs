using System;

namespace Balances.Domain.Entities
{
    /// <summary>
    /// Represents a balance of an asset.
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// The asset id.
        /// </summary>
        public string AssetId { get; set; }

        /// <summary>
        /// The amount of balance.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The amount that currently are reserved.
        /// </summary>
        public decimal Reserved { get; set; }

        /// <summary>
        /// The date and time of balance.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
