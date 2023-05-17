using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickedKnowledgeHub.Model;
using TrickedKnowledgeHub.Model.Repo;

namespace TrickedKnowledgeHub.ViewModel
{
    public class UpdatePageVM
    {
        ExerciseRepository exerciseRepository = new();

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] Material { get; set; }

        public Game? Game { get; set; }

        public FocusPoint? FocusPoint { get; set; }

        public Rating? rating { get; set; }

        public void Update()
        {
            exerciseRepository.Update(Id, Title, Description, Material, Game, FocusPoint, rating);
        }
    }
}
