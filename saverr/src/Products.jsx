import { useEffect, useState } from "react";
import "./App.css";
import ProductTable from "./ProductTable";
import axios from "../node_modules/axios/index";

function Products() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState(""); // for input
  const [query, setQuery] = useState("Havredrik"); // default search
  const [onlyOrganic, setOnlyOrganic] = useState(false);

  useEffect(() => {
    fetchProducts();
  }, [query]);

  const fetchProducts = async () => {
    try {
      const response = await axios.get(
        `https://localhost:7076/Product?search=${encodeURIComponent(
          query
        )}&organicOnly=${onlyOrganic}`
      );
      if (response.status != 200) {
        throw new Error("Failed to fetch products");
      }

      const data = await response.data;

      // Assuming your API returns an array of products:
      setProducts(data);
      console.log(data);
    } catch (error) {
      console.error("Error fetching products:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      setQuery(searchTerm);
    }
  };

  if (loading) {
    return (
      <div className="card">
        <p>Henter produkter...</p>
      </div>
    );
  }

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        
        margin: "auto",
        padding: "2rem",
      }}
    >
      <div
        className="card"
        style={{
          alignItems: "center", // centers items horizontally in column
          width: "100%",
          padding: "1rem",
        }}
      >
        <input
          type="text"
          placeholder="Søg efter produktnavn..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          onKeyDown={handleKeyDown}
          style={{ width: "50%", padding: "10px", marginBottom: "1rem" }}
        />
        <label>
          <input
            type="checkbox"
            checked={onlyOrganic}
            onChange={() => setOnlyOrganic(!onlyOrganic)}
            style={{ alignContent: "center" }}
          />
          &nbsp; Vis kun økologiske produkter
        </label>
      </div>
      <div
        style={{
          flex: 1,
          width: "100%",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        {loading ? (
          <div className="">
            <p>Henter produkter...</p>
          </div>
        ) : products.length > 0 ? (
          <div
            style={{ width: "100%", display: "flex", justifyContent: "center" }}
          >
            <ProductTable products={products} />
          </div>
        ) : (
          <div className="">
            <p>Ingen produkter...</p>
          </div>
        )}
      </div>
    </div>
  );
}

export default Products;
