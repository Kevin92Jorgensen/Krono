import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { useState } from 'react';
import { useEffect } from 'react';

function createData(id, gtid, name, brand, imageMimeType, imageData, price, shop) {
    return { id, gtid, name, brand, imageMimeType, imageData, price, shop };
}

//const rows = [
//    createData('Frozen yoghurt', 159, 6.0, 24, 4.0),
//    createData('Ice cream sandwich', 237, 9.0, 37, 4.3),
//    createData('Eclair', 262, 16.0, 24, 6.0),
//    createData('Cupcake', 305, 3.7, 67, 4.3),
//    createData('Gingerbread', 356, 16.0, 49, 3.9),
//];

export default function BasicTable({ products }) {
    var rows = [];
    const [favorites, setFavorites] = useState([]);

    for(var product of products) {
        const lowest = product.priceEntries.reduce((min, current) =>
            current.price < min.price ? current : min
        );
        rows.push(createData(product.id, product.gtid, product.name, product.brand, product.imageMimeType, product.imageData, (lowest.price / 100), lowest.shop.name))
        if (product.favorite) {
            setFavorites([...favorites],product.gtid)
        }
    }
    useEffect(() => {
        const favs = products
            .filter(p => p.favorite === true)
            .map(p => p.gtid);
        setFavorites(favs);
    }, [products]);


    const onToggleFavorite = (gtid) => {
        setFavorites((prev) =>
            prev.includes(gtid)
                ? prev.filter((id) => id !== gtid)
                : [...prev, gtid]
        );
    };
    

    return (
        <TableContainer component={Paper}>
            <Table sx={{ width: '100%' }} aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Navn</TableCell>
                        <TableCell align="right">Mærke</TableCell>
                        <TableCell align="right">Bedste pris</TableCell>
                        <TableCell align="right">Butik</TableCell>
                        <TableCell align="right">
                           </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows.sort((a, b) => a.price - b.price).map((row) => (
                        <TableRow
                            key={row.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {row.name}
                            </TableCell>
                            <TableCell align="right">{row.brand}</TableCell>
                            <TableCell align="right">{row.price} kr.</TableCell>
                            <TableCell align="right">{row.shop}</TableCell>
                            <TableCell align="right">
                                {row.imageData &&
                                    <img
                                        src={"data:" + row.imageMimeType + ";base64," + row.imageData}
                                        alt={row.name}
                                    style={{
                                        width: 60, height: 'auto',
        transition: 'transform 0.2s ease-in-out',
                                        cursor: 'zoom-in' }}

                                    onMouseOver={(e) => (e.currentTarget.style.transform = 'scale(2.5)')}
                                    onMouseOut={(e) => (e.currentTarget.style.transform = 'scale(1)')}
                                    />}</TableCell>
                            <TableCell align="right">
                                <button onClick={() => onToggleFavorite(row.gtid)}>
                                    {favorites.includes(row.gtid) ? '⭐' : '☆'}
                                </button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
