using Core.Models;
using System.Text.Json.Serialization;

namespace Core.DTOs
{
    public abstract class BaseEntityDTO
    {
        private int _id;

        public int Id { get => _id; set => _id = value; }

    }
}
