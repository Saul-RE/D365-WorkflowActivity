using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.Threading;
using Microsoft.Xrm.Sdk.Query;
using System.Xml.Linq;

namespace WorkFlowActivityVistaAprobadores
{
    public class VistaAprobadores : CodeActivity
    {
        [RequiredArgument]
        [Input("Campo Busqueda")]
        [ReferenceTarget("lnd_autorizadorcredito")]
        public InArgument<EntityReference> AutorizadorCredito { get; set; }

        [Input("Nombre aprobador")]
        public InArgument<string> NombreAprobador { get; set; }

        [Output("Salida Nombre")]
        public OutArgument<string> Name { get; set; }



        protected override void Execute(CodeActivityContext executionContext)
        {

            //Crear Contexto
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            //Obtener Id de la entidad relacionada y nombre del aprobador
            EntityReference autorizadorcredito = AutorizadorCredito.Get<EntityReference>(executionContext);
            string nombreaprobador = NombreAprobador.Get<string>(executionContext);
            string nombres = string.Empty;

            QueryExpression qeFlujoautorizador = new QueryExpression()
            {
                EntityName = "lnd_autorizadorcredito",
                ColumnSet = new ColumnSet("lnd_vistaaprobadores")
            };                             

            EntityCollection ecFlujoautorizador = service.RetrieveMultiple(qeFlujoautorizador);
            if (ecFlujoautorizador.Entities.Count == 0)
            {
                return;
            }

            //foreach (Entity e in ecFlujoautorizador.Entities)
            //{
            //    if (e.Id == autorizadorcredito.Id)
            //    {
            //        if (e.Attributes["lnd_vistaaprobadores"].ToString().Contains(nombreaprobador))
            //        {
                        
            //            service.Update(e);
            //            nombres = e.GetAttributeValue<string>("lnd_vistaaprobadores");
            //        }
            //    }
            //}
            nombreaprobador = string.Empty;
            Name.Set(executionContext, nombreaprobador);

        }
    }
}
