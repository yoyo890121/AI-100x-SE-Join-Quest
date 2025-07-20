@double11_promotion
Feature: 雙十一優惠活動
  As a shopper
  I want the system to calculate my order total with applicable promotions
  So that I can understand how much to pay and what items I will receive
  同一種商品每買 10 件，則該 10 件同種商品的價格總和會享有 20% 的折扣

  Background:
    Given the Double 11 promotion is active

  Scenario: 購買 12 件同種商品享有批量折扣
    When a customer places an order with:
      | productName | category | quantity | unitPrice |
      | 襪子          | apparel  | 12       | 100       |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 1200          | 200      | 1000        |
    And the customer should receive:
      | productName | quantity |
      | 襪子          | 12       |

  Scenario: 購買 27 件同種商品享有多組批量折扣
    When a customer places an order with:
      | productName | category | quantity | unitPrice |
      | 襪子          | apparel  | 27       | 100       |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 2700          | 400      | 2300        |
    And the customer should receive:
      | productName | quantity |
      | 襪子          | 27       |

  Scenario: 購買 10 種不同商品各一件無折扣
    When a customer places an order with:
      | productName | category  | quantity | unitPrice |
      | 商品A        | apparel  | 1        | 100       |
      | 商品B        | apparel  | 1        | 100       |
      | 商品C        | apparel  | 1        | 100       |
      | 商品D        | apparel  | 1        | 100       |
      | 商品E        | apparel  | 1        | 100       |
      | 商品F        | apparel  | 1        | 100       |
      | 商品G        | apparel  | 1        | 100       |
      | 商品H        | apparel  | 1        | 100       |
      | 商品I        | apparel  | 1        | 100       |
      | 商品J        | apparel  | 1        | 100       |
    Then the order summary should be:
      | originalAmount | discount | totalAmount |
      | 1000          | 0        | 1000        |
    And the customer should receive:
      | productName | quantity |
      | 商品A         | 1        |
      | 商品B         | 1        |
      | 商品C         | 1        |
      | 商品D         | 1        |
      | 商品E         | 1        |
      | 商品F         | 1        |
      | 商品G         | 1        |
      | 商品H         | 1        |
      | 商品I         | 1        |
      | 商品J         | 1        |
