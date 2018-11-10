using System.Collections.Generic;

namespace BusinessId.Specification
{
    public class BusinessIdSpecification : ISpecification<string>
    {
        private BusinessId _businessId;

        public BusinessIdSpecification()
        {
            _businessId = new BusinessId();
        }

        public IEnumerable<string> ReasonsForDissatisfaction
        {
            get
            {
                return _businessId.Errors;
            }
        }

        public bool IsSatisfiedBy(string entity)
        {
            _businessId.Value = entity;
            return _businessId.VerifyValue();
        }
    }
}
