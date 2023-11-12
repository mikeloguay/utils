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
        {customers.map(c => (<li key={c.id}>Name: {c.name} - Age: {c.age}</li>))}
      </ol>
    </>
  );
};

export default CustomerList;
