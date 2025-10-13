import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import type { Furniture } from '../models/furniture';
import { furnitureService } from '../services/furnitureService';
import styles from './FurnitureDetails.module.css';

const FurnitureDetails = () => {
  const [furniture, setFurniture] = useState<Furniture | null>(null);
  const [loading, setLoading] = useState(true);
  const { id } = useParams<{ id: string }>();

  useEffect(() => {
    if (id) {
      furnitureService.getFurniture(parseInt(id))
        .then((response) => {
          setFurniture(response.data);
          setLoading(false);
        })
        .catch((error) => {
          console.error('Error fetching furniture details:', error);
          setLoading(false);
        });
    }
  }, [id]);

  if (loading) {
    return (
      <div className="container">
        <div className="text-center">Loading...</div>
      </div>
    );
  }

  if (!furniture) {
    return (
      <div className="container">
        <div className="text-center">
          <p>Furniture not found</p>
        </div>
      </div>
    );
  }

  return (
    <div className="container">
      <div className="row space-top">
        <div className="col-md-12">
          <h1>Furniture Details</h1>
        </div>
      </div>
      
      {furniture && (
        <div className="row space-top">
          <div className="col-md-4">
            <div className="card text-white bg-primary">
              <div className="card-body">
                <blockquote className="card-blockquote">
                  <img src={furniture.image} alt={`${furniture.make} ${furniture.model}`} className={styles.img} />
                </blockquote>
              </div>
            </div>
          </div>
          <div className="col-md-4">
            <p>Make: {furniture.make}</p>
            <p>Model: {furniture.model}</p>
            <p>Year: {furniture.year}</p>
            <p>Description: {furniture.description}</p>
            <p>Price: {furniture.price}</p>
            {furniture.material && (
              <p>Material: {furniture.material}</p>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default FurnitureDetails;