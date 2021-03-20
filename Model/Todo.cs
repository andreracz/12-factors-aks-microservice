using System;

namespace aks_12_factors_microservice.Model
{
    public class Todo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Complete { get; set; }
    }
}
