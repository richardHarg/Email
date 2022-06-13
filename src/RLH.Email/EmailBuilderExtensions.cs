using RLH.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                var finalPopulated = string.Format(templateFile, templateValues);
                return ResultOf<string>.Success(finalPopulated);
            }
            catch (Exception populateException)
            {
                return ResultOf<string>.Error(populateException.Message);
            }
        }

    }
}
