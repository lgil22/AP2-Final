using Microsoft.EntityFrameworkCore;
using Blazor.DAL;
using Blazor.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blazor.BLL
{
    public class CobrosBLL
    {
        public static bool Guardar(Cobros cobro)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Cobros.Add(cobro) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Cobros cobro)
        {
            bool paso = false;
            var ant = Buscar(cobro.CobroId);
            Contexto contexto= new Contexto();

            try
            {
                foreach (var item in ant.Detalle)
                {
                    var auxCobro = contexto.Cobros.Find(item.CobroId);
                    if (!cobro.Detalle.Exists(d => d.Id == item.CobroId))
                    {
                        if (auxCobro != null)
                            contexto.Entry(item).State = EntityState.Deleted;
                    }

                }

                foreach (var item in cobro.Detalle)
                {
                    if (item.Id == 0)
                    {
                        contexto.Entry(item).State = EntityState.Added;


                    }
                    else
                        contexto.Entry(item).State = EntityState.Modified;
                }


                contexto.Entry(cobro).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = CobrosBLL.Buscar(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Cobros Buscar(int id)
        {
            Contexto db = new Contexto();
            Cobros cobro = new Cobros();

            try
            {
                cobro = db.Cobros.Include(x => x.Detalle)
                    .Where(x => x.CobroId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return cobro;
        }

        public static List<Cobros> GetList(Expression<Func<Cobros, bool>> cobro)
        {
            List<Cobros> lista = new List<Cobros>();
            Contexto contexto = new Contexto();

            try
            {
                lista = contexto.Cobros.Where(cobro).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}