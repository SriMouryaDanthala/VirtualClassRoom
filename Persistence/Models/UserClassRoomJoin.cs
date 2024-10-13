﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class UserClassRoomJoin
    {
        public Guid ClassRoomId { get; set; }
        public Guid UserId { get; set; }

        public UserModel User { get; set; }

        public ClassRoomModel ClassRoom { get; set; }
    }
}
