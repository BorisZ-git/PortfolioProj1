using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAnimTreasure : MonoBehaviour
{
    public Camera cam;
    public AudioSource slam;
    public float duration, magnitude, noize;
    private void Awake()
    {
        slam = GetComponent<AudioSource>();
    }
    public void ShakeCamera()
    {
        slam.Play();
        StartCoroutine(ShakeCameraCor());
    }
    private IEnumerator ShakeCameraCor()
    {
        //Инициализируем счётчиков прошедшего времени
        float elapsed = 0f;
        //Сохраняем стартовую локальную позицию
        Vector3 startPosition = cam.transform.localPosition;
        //Генерируем две точки на "текстуре" шума Перлина
        Vector2 noizeStartPoint0 = Random.insideUnitCircle * noize;
        Vector2 noizeStartPoint1 = Random.insideUnitCircle * noize;

        //Выполняем код до тех пор пока не иссякнет время
        while (elapsed < duration)
        {
            //Генерируем две очередные координаты на текстуре Перлина в зависимости от прошедшего времени
            Vector2 currentNoizePoint0 = Vector2.Lerp(noizeStartPoint0, Vector2.zero, elapsed / duration);
            Vector2 currentNoizePoint1 = Vector2.Lerp(noizeStartPoint1, Vector2.zero, elapsed / duration);
            //Создаём новую дельту для камеры и умножаем её на длину дабы учесть желаемый разброс
            Vector2 cameraPostionDelta = new Vector2(Mathf.PerlinNoise(currentNoizePoint0.x, currentNoizePoint0.y), 
                Mathf.PerlinNoise(currentNoizePoint1.x, currentNoizePoint1.y));
            cameraPostionDelta *= magnitude;

            //Перемещаем камеру в нувую координату
            cam.transform.localPosition = startPosition + (Vector3)cameraPostionDelta;

            //Увеличиваем счётчик прошедшего времени
            elapsed += Time.deltaTime;
            //Приостанавливаем выполнение корутины, в следующем кадре она продолжит выполнение с данной точки
            yield return null;
        }
        cam.transform.localPosition = startPosition;
    }
}
