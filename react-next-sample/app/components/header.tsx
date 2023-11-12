import Image from "next/image";

const Header = () => {
  return (
    <span>
      <Image
        src="/vercel.svg"
        alt="Vercel Logo"
        width={100}
        height={24}
        priority
      />
      <h1>Customers page</h1>
    </span>
  );
};

export default Header;
