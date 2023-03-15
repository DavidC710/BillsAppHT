
namespace BillsApp.Domain.Entities.Base
{
    public abstract class BaseEntity
    {
        private int _Id { get; set; }

        public virtual int Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        public bool IsTransient()
        {
            return Id == 0;
        }
    }
}
