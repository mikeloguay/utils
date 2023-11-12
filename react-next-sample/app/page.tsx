import Image from 'next/image'
import Header from './components/header';
import CustomerList from './components/customerList';
import { Customer } from './components/types';

const customersFromApi: Customer[] = [
  { id: 1, name: 'John', age: 25 },
  { id: 2, name: 'Jane', age: 24 },
  { id: 3, name: 'Jack', age: 26 },
];

export default function Home() {
  return (
    <>
      <Header />
      <CustomerList customers={customersFromApi} />
    </>
 );
}
