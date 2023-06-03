﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace SportEventManager.Core.EventAggregate.Specifications;
public class EventsWithItemsSpec : Specification<Event>
{
  public EventsWithItemsSpec()
  {
    Query
        .Include(selectEvent => selectEvent.Teams)
        .Include(selectEvent => selectEvent.Stadiums)
        .Include(selectEvent => selectEvent.Matches)
        .Where(selectEvent => !selectEvent.IsArchived)
        .OrderByDescending(selectEvent => selectEvent.StartTime);
  }
}
