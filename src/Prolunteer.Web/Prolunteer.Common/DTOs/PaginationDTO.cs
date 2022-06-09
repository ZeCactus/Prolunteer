using System;
using System.Collections.Generic;

namespace Prolunteer.Common.DTOs
{
    public class PaginationDTO<TElement>
    {
        public PaginationDTO(List<TElement> elements, int totalElementsCount)
        {
            this.Elements = elements;
            this.TotalElementsCount = totalElementsCount;
        }

        public List<TElement> Elements { get; set; }
        public int TotalElementsCount { get; set; }
    }
}
