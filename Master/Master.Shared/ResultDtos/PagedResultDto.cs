using System;
using System.Collections.Generic;

namespace Lila.Platform.Shared.ResultDtos
{
    [Serializable]
    public class PagedResultDto<T>
    {
        /// <summary>
        /// Total count of Items
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Items of the current page if specified
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }

        public PagedResultDto()
        {
            Items = new List<T>();
        }

        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
