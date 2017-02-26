// ©2017 https://digital-learning.crealogix.com/ | Product: CLX.Evento | Class1.cs
namespace Gaswerk.RouteApp.Models
{
    public class EditingSchwierigkeitModel
    {
       
        /// <inheritdoc />
        public EditingSchwierigkeitModel(Schwierigkeit current, string updateTargetId)
        {
            Current = current;
            UpdateTargetId = updateTargetId;
        }

        public Schwierigkeit Current { get; set; }
        public string UpdateTargetId { get; private set; }

    }
}