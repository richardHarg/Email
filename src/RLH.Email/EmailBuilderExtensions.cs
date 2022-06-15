using RLH.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RLH.Email
{
    public static class EmailBuilderExtensions
    {
        public static Result.Result SetHTMLBodyFromTemplate(this EmailBuilder emailBuilder, string pathToTemplateFile,ICollection<string> templateValues)
        {
            if (emailBuilder is null)
            {
                throw new ArgumentNullException(nameof(emailBuilder));
            }

            if (string.IsNullOrEmpty(pathToTemplateFile))
            {
                throw new ArgumentException($"'{nameof(pathToTemplateFile)}' cannot be null or empty.", nameof(pathToTemplateFile));
            }

            if (templateValues is null || templateValues.Any() == false)
            {
                throw new ArgumentNullException(nameof(templateValues));
            }

            ResultOf<string> result = PopulateTemplateValues(pathToTemplateFile, templateValues);

            if (result.Status == ResultStatus.Success)
            {
                emailBuilder.SetHTMLBody(result.Value);
                return Result.Result.Success();
            }
            else
            {
                return Result.Result.Error(result.Errors);
            }
        }

        private static ResultOf<string> PopulateTemplateValues(string pathToTemplateFile,ICollection<string> templateValues)
        {
            string templateFile;

            try
            {
                using (StreamReader SourceReader = File.OpenText(pathToTemplateFile))
                {
                    templateFile = SourceReader.ReadToEnd();
                }
            }
            catch (Exception templateError)
            {
                return ResultOf<string>.Error(templateError.Message);
            }

            try
            {
                return ResultOf<string>.Success(FillTemplateValues(templateFile, templateValues));
            }
            catch (Exception populateException)
            {
                return ResultOf<string>.Error(populateException.Message);
            }
        }

        private static string FillTemplateValues(string body,ICollection<string> templateValues)
        {
            for (int i = 0; i < templateValues.Count; i++)
            {
                var match = "{" + i + "}";

                body = body.Replace(match, templateValues.ElementAt(i));
            }

            return body;
        }

    }
}
