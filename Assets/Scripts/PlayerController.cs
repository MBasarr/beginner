using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class PlayerController : MonoBehaviour
{
  // Oyuncunun Rigidbody bileşeni.
    private Rigidbody rb; 
    
    // Toplanan "Coin" nesnelerinin sayısını tutan değişken.
    private int count;
    
    // Toplanan "Coin" nesnelerinin sayısını gösteren UI metin bileşeni.
    public TextMeshProUGUI countText;
    
    // Kazanma metnini gösteren UITM nesnesi.
    public GameObject winTextObject;

    // X ve Y eksenlerinde hareket.
    private float movementX;
    private float movementY;

    // Oyuncunun hareket hızı.
    public float speed = 0;
    
    void Start()
    {
        // Oyuncuya ekli Rigidbody bileşenini al ve sakla.
        rb = GetComponent<Rigidbody>();
        
        // Sayacı sıfırla.
        count = 0;
        
        // Sayaç gösterimini güncelle.
        SetCountText();
        
        // İlk olarak kazanma metnini devre dışı bırak.
        winTextObject.SetActive(false);
    }
 
    // Bu fonksiyon, bir hareket girdisi algılandığında çağrılır.
    void OnMove(InputValue movementValue)
    {
        // Giriş değerini hareket için bir Vector2'ye dönüştür.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Hareketin X ve Y bileşenlerini sakla.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate, her sabit kare hızında bir kez çağrılır.
    private void FixedUpdate() 
    {
        // X ve Y girdilerini kullanarak 3D bir hareket vektörü oluştur.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Oyuncuyu hareket ettirmek için Rigidbody'ye kuvvet uygula.
        rb.AddForce(movement * speed); 
    }

    void OnTriggerEnter(Collider other) 
    {
        // Oyuncunun çarptığı nesnenin "Coins" etiketine sahip olup olmadığını kontrol et.
        if (other.gameObject.CompareTag("Coins")) 
        {
            // Çarpılan nesneyi devre dışı bırak (görünmez yap).
            other.gameObject.SetActive(false);
            
            // Toplanan "Coin" nesnelerinin sayısını bir artır.
            count = count + 1;
            
            // Sayaç gösterimini güncelle.
            SetCountText();
        }
    }
    
    // Toplanan "Coin" nesnelerinin sayısını güncelleyen fonksiyon.
    void SetCountText()
    {
        // Sayaç metnini, mevcut sayıyla güncelle.
        countText.text = "Count:" + count.ToString();
        
        // Eğer tüm coinler toplanırsa;
        if (count >= 11)
        {
            // "YOU WIN" metnini göster.
            winTextObject.SetActive(true);
            
            //Enemy i yok et
            Destroy(GameObject.FindWithTag("Enemy"));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //kaybetme yazısı-enemy çarparsa 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //playerı yok et
            Destroy(gameObject);
            //you louse yaz
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose";
        }
    }
}
