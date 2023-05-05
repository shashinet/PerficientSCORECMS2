import React from 'react';
import styles from './searchLoading.module.scss';

const SearchLoading = () => (
  <div className="container">
    <div className="w-full">
      <div className={styles.searchResultsLoading}>
        <div className="search-results-by-letter-section">
          <span className="loading-letter" />
          <div className="interior-pages-wrapper">
            <span className="loading-title" />
            <div className="interior-pages-cards">
              <div className="card" />
              <div className="card" />
              <div className="card" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
);

export default React.memo(SearchLoading);
