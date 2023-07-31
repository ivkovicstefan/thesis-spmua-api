﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPMUA.Model.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public int EntityId { get; set; }

        public EntityNotFoundException(int entityId)
        {
            EntityId = entityId;
        }
    }
}
