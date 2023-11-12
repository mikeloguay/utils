import { Customer } from "./types";

interface CustomerDetailsProps {
    customer: Customer;
}

const CustomerDetails = (props: CustomerDetailsProps) => {

    const {customer} = props;

    return (<li key={customer.id}>Name: {customer.name} - Age: {customer.age}</li>)
};

export default CustomerDetails;