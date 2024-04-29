using System.ComponentModel.DataAnnotations;

namespace Soccer.Models
{   
    public class Teams
    {
        public Teams()
        {
            this.Players = new HashSet<Players>();
        }
    
        public int Id { get; set; }
        [Required(ErrorMessage = "���� ������ ���� �����������")]
        public string Name { get; set; }
        [Required(ErrorMessage = "���� ������ ���� �����������")]
        public string Coach { get; set; }
        public ICollection<Players>? Players { get; set; }
    }
}
