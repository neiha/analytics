using analytics.Models.DTO;
using FluentNHibernate.Mapping;

namespace analytics.Models.DAO.Mapping
{
    public class TiempoMap:ClassMap<Tiempo>
    {
        public TiempoMap()
        {
            Table("tiempo");
            Id(x => x.IdTiempo).Column("idTiempo");
            Map(x => x.Nombre).Column("nombre");
        }
    }
}
