import React from 'react';
import PropTypes from 'prop-types';

const SearchBoxBlock = React.forwardRef(({ children, title, description }, ref) => (
  <section className="search-box-block">
    <div className="heading">
      <div className="container">
        <div className="w-full">
          <div className="section-header">
            <h2>{title}</h2>
            <div className="header-description" dangerouslySetInnerHTML={{ __html: description }} />
          </div>
        </div>
      </div>
    </div>
    <div className="search">
      <div className="container">
        <div className="w-full">
          <div className="search-box" ref={ref}>
            {children}
          </div>
        </div>
      </div>
    </div>
  </section>
));

SearchBoxBlock.propTypes = {
  title: PropTypes.string.isRequired,
  description: PropTypes.string.isRequired,
  children: PropTypes.oneOfType([PropTypes.arrayOf(PropTypes.node), PropTypes.node]).isRequired,
};

export default SearchBoxBlock;
