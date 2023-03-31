using System;
using System.Collections.Generic;

namespace Master.Shared.ResultDtos
{
    [Serializable]
    public class ListResultDto<T>
    {
        /// <summary>
        /// Items of the list
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }

        public ListResultDto()
        {
            Items = new List<T>();
        }

        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
