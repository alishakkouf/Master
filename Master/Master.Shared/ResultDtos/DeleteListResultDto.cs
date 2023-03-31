using System.Collections.Generic;

namespace Master.Shared.ResultDtos
{
    public class DeleteListResultDto : DeleteListResultDto<int>
    {
    }

    public class DeleteListResultDto<T>
    {
        /// <summary>
        /// Entity ids successfully deleted
        /// </summary>
        public List<T> DeletedIds { get; set; } = new List<T>();

        /// <summary>
        /// Entity ids failed to be deleted
        /// </summary>
        public List<T> FailedIds { get; set; } = new List<T>();

        public string Message { get; set; }
    }
}
