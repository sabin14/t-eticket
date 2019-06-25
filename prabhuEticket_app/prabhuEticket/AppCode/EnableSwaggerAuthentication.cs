using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace prabhuEticket.AppCode
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            //System.Diagnostics.Trace.WriteLine(apiDescription.RelativePath + "=paath");
            operation.parameters.Add(new Parameter
            {
                name = "api-key",
                @in = "header",
                description = "Api key received from provider",
                type = "string",
                required = true
            });
            if (apiDescription.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower() != "authenticateapi")
            {
                if (apiDescription.RelativePath.Contains("api/v1/authentication") == false)
                {
                    operation.parameters.Add(new Parameter
                    {
                        name = "access-token",
                        @in = "header",
                        description = "Token generated",
                        type = "string",
                        required = true
                    });
                }
            }
            //if (apiDescription.RelativePath.Contains("api/v1/authentication"))
            //{

            //    operation.parameters.Add(new Parameter
            //    {
            //        name = "authorization",
            //        @in = "header",
            //        description = "Basic Authentication[base64({ username}:{password})]",
            //        type = "string",
            //        required = true
            //    });
            //}
            //else
            //{
            //    operation.parameters.Add(new Parameter
            //    {
            //        name = "OAuthentication",
            //        @in = "header",
            //        description = "Token",
            //        type = "string",
            //        required = true
            //    });
            //}
        }
    }
    public class DocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// This method is for applying the filter
        /// </summary>
        /// <param name="swaggerDoc">Swagger Document</param>
        /// <param name="schemaRegistry">Schema Registry</param>
        /// <param name="apiExplorer">API Explorer</param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var methods = swaggerDoc.paths.Select(i => i.Value);
            List<string> tags = new List<string>();
            foreach (var method in methods)
            {
                if (method.delete != null)
                {
                    tags.AddRange(method.delete.tags);
                }

                if (method.get != null)
                {
                    tags.AddRange(method.get.tags);
                }

                if (method.put != null)
                {
                    tags.AddRange(method.put.tags);
                }

                if (method.post != null)
                {
                    tags.AddRange(method.post.tags);
                }

                if (method.patch != null)
                {
                    tags.AddRange(method.patch.tags);
                }
            }

            swaggerDoc.tags = new List<Tag>();
            foreach (var tag in tags)
            {
                swaggerDoc.tags.Add(new Tag() { name = tag, description = "This is a group of methods for " + tag });
            }
        }
    }
    public class FormatXmlCommentProperties : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            operation.description = Formatted(operation.description);
            operation.summary = Formatted(operation.summary);
        }

        private string Formatted(string text)
        {
            if (text == null) return null;
            var stringBuilder = new StringBuilder(text);

            return stringBuilder
                .Replace("<para>", "<p>")
                .Replace("</para>", "</p>")
                .ToString();
        }
    }

}