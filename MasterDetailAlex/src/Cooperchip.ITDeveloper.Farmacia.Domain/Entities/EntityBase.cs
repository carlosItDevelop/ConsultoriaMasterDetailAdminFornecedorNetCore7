using System;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase() => Id = Guid.NewGuid();
        public Guid Id { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;
            return Id.Equals(compareTo.Id);
        }
        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }
        public static bool operator !=(EntityBase a, EntityBase b) => !(a == b);
        public override int GetHashCode() => GetType().GetHashCode() * 13 + Id.GetHashCode();
        public override string ToString() => GetType().Name + " [Id=" + Id + "]";
    }
}


// DataCadastro, DataModificacao, UsuarioAcao

// Criar Comparações entre entidades --/ Para refletir
// Comparando Identidade e não instância de uma classe de mesmo tipo;

// Maçã = OutraMaçã ????
// maca = new Maca()  => Instancia de uma Maçã
// Pessoa xpto = new {id=1, nome=Carlos, cpf=38487432943};
// Pessoa xpto2 = new {id=2, nome=Marcos, cpf=77487432943};
