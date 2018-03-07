using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Pundit.Interlude
{
    public sealed class PagingOptions
    {
        public const int MaxPageSize = 100;

        public PagingOptions() : this(0, MaxPageSize)
        {
        }

        public PagingOptions(int? offset, int? limit)
        {
            Offset = offset;
            Limit = limit;
        }

        [FromQuery]
        [Range(1, int.MaxValue, ErrorMessage = "Offset must be greater than 0.")]
        public int? Offset { get; set; }

        [FromQuery]
        [Range(1, MaxPageSize, ErrorMessage = "Limit must be greater than 0 and less than 100.")]
        public int? Limit { get; set; }
    }
}
