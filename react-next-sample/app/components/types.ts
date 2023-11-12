export interface CustomerListProps {
  customers: Customer[];
}

export interface Customer {
  id: number;
  name: string;
  age: number;
}
