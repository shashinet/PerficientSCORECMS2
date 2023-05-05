/* eslint-disable react/no-unknown-property */
import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function Breadcrumbs(props) {
  const { icon } = props;
  return (
    <section className={['breadcrumb-wrapper', icon ? 'has-icon' : ''].join(' ')}>
      <div className="container">
        <div className="w-full">
          <ol
            className="breadcrumb"
            itemScope
            itemType="http://schema.org/BreadcrumbList"
          >
            <li
              className="breadcrumb-item"
              itemProp="itemListElement"
              itemScope
              itemType="http://schema.org/BreadcrumbList"
            >
              <a href="/Users/mikebaker/Public" itemProp="item">
                <span itemProp="name">Services</span>
              </a>
              <meta itemProp="position" content="1" />
            </li>
            <li
              className="breadcrumb-item"
              itemProp="itemListElement"
              itemScope
              itemType="http://schema.org/BreadcrumbList"
            >
              <a href="/Users/mikebaker/Public" itemProp="item">
                <span itemProp="name">Cancer Care</span>
              </a>
              <meta itemProp="position" content="2" />
            </li>
            <li
              className="breadcrumb-item"
              itemProp="itemListElement"
              itemScope
              itemType="http://schema.org/BreadcrumbList"
            >
              <a href="/Users/mikebaker/Public" itemProp="item">
                <span itemProp="name">Breast Cancer</span>
              </a>
              <meta itemProp="position" content="3" />
            </li>
          </ol>
        </div>
      </div>
    </section>
  );
}

Breadcrumbs.propTypes = {
  icon: PropTypes.bool,
};

Breadcrumbs.defaultProps = {
  icon: false,
};
