import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import type { Furniture } from '../models/furniture';
import { furnitureService } from '../services/furnitureService';
import styles from './FurnitureUser.module.css';

const FurnitureUser = () => {
  const [furnitures, setFurnitures] = useState<Furniture[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getFurnituresForUser();
  }, []);

  const getFurnituresForUser = () => {
    setLoading(true);
    furnitureService.getFurnituresForUser()
      .then((response) => {
        const data = response.data;
        setFurnitures(Array.isArray(data) ? data : data.items);
        setLoading(false);
      })
      .catch(() => {
        setLoading(false);
      });
  };

  const deleteFurniture = (id: number) => {
    furnitureService.deleteFurniture(id)
      .then(() => {
        getFurnituresForUser();
      });
  };

  return (
    <div className="container">
      <div className="row space-top">
        <div className="col-md-12">
          <h1>Profile Page</h1>
          <p>Listing your furniture.</p>
        </div>
      </div>

      {loading && (
        <div className="row">
          <div className="col-md-4"></div>
          <div className="col-md-4 text-center">Loading...</div>
          <div className="col-md-4"></div>
        </div>
      )}

      {!loading && furnitures.length === 0 ? (
        <div className="row space-top">
          <div className="col-md-12">
            No furnitures
          </div>
        </div>
      ) : (
        <div className="row space-top">
          {furnitures.map((furniture) => (
            <div key={furniture.id} className="col-md-4">
              <div className="card text-white bg-primary">
                <div className={`card-body ${styles.cardBody}`}>
                  <blockquote className={`card-blockquote ${styles.blockquote}`}>
                    <img src={furniture.image} alt={`${furniture.make} ${furniture.model}`} className={styles.img} />
                    <p>{furniture.description}</p>
                    <div className="pull-right">
                      <Link
                        to={`/furniture/details/${furniture.id}`}
                        className={`btn btn-info ${styles.btnInfo}`}
                      >
                        Details
                      </Link>
                      <a
                        onClick={() => deleteFurniture(furniture.id)}
                        className={`btn btn-danger`}
                      >
                        Delete
                      </a>
                    </div>
                  </blockquote>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default FurnitureUser;