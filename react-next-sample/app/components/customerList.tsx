import CustomerDetails from "./customerDetails";
import { Customer } from "./types";

interface CustomerListProps {
    customers: Customer[];
}

const CustomerList = (props: CustomerListProps) => {

  const {customers} = props;

  return (
    <>
      <p>This is a list of customers</p>
      <ol>
        {customers.map(c => (<CustomerDetails key={c.id} customer={c} />))}
      </ol>
    </>
  );
};

export default CustomerList;
