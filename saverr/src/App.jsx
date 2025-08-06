import { BrowserRouter, Link, Route, Router, Routes } from 'react-router-dom'
import './App.css'
import Products from './Products'
import BarcodeScanner from './BarcodeScanner'

function App() {
  

    return (
            
        <BrowserRouter>
          <nav className="navbar" style={{ backgroundColor: '#eee' }}>
              <Link to="/" style={{ marginRight: '15px' }}>🔍 Search</Link>
              <Link to="/scan">📷 Scan Barcode</Link>
          </nav>

<div className='content'>
          <Routes>
              <Route path="*" element={<Products />} />
              <Route path="/scan" element={<BarcodeScanner />} />
          </Routes>
          </div>
      </BrowserRouter>
      
  )
}

export default App

