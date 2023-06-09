﻿using Core.Abstractions.Repository;
using Core.DTOs;
using Core.Models;
using Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(ILogger logger, IGamblingContext context) : base(logger, context)
        {
        }
    }
}
