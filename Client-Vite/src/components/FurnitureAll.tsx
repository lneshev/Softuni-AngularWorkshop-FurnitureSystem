import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import type { Furniture } from '../models/furniture';
import { furnitureService } from '../services/furnitureService';
import { authService } from '../services/authService';
import styles from './FurnitureAll.module.css';

const FurnitureAll = () => {
  const [furnitures, setFurnitures] = useState<Furniture[]>([]);
  const [loading, setLoading] = useState(true);
  const [isUserAdmin, setIsUserAdmin] = useState(false);

  useEffect(() => {
    getAllFurnitures();
    const user = authService.getUserFromToken();
    setIsUserAdmin(user.roles[0] === 'Super Admin');
  }, []);

  const getAllFurnitures = () => {
    setLoading(true);
    let timer = setTimeout(() => {
      furnitureService.getAllFurnitures()
        .then((response) => {
          const data = response.data;
          setFurnitures(Array.isArray(data) ? data : data.items);
          setLoading(false);
        })
        .catch(() => {
          setLoading(false);
        });
    }, 1000);

    return () => {
      clearTimeout(timer);
    }
  };

  const deleteFurniture = (id: number) => {
    furnitureService.deleteFurniture(id)
      .then(() => {
        getAllFurnitures();
      });
  };

  return (
    <div className="container">
      <div className="row space-top">
        <div className="col-md-12">
          <h1>Welcome to Furniture System</h1>
          <p>Select furniture from the catalog to view details.</p>
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
                    <p>{furniture.description.length > 11
                      ? `${furniture.description.slice(0, 11)}...`
                      : furniture.description
                    }</p>
                    <div className="pull-right">
                      <Link
                        to={`/furniture/details/${furniture.id}`}
                        className={`btn btn-info ${styles.btnInfo}`}
                      >
                        Details
                      </Link>
                      {isUserAdmin && (
                        <Link
                          to={`/furniture/edit/${furniture.id}`}
                          className={`btn btn-info ${styles.btnInfo}`}
                        >
                          Edit
                        </Link>
                      )}
                      {isUserAdmin && (
                        <a
                          onClick={() => deleteFurniture(furniture.id)}
                          className="btn btn-danger"
                        >
                          Delete
                        </a>
                      )}
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

export default FurnitureAll;